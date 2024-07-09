(function() {
	var Search =
	{
		init: function(selector, caseSensitive)
		{
			var jQuerySelect;
			caseSensitive = caseSensitive ? true : false;
			selector = selector ? selector : document.body; 
			if (typeof selector === "string" || typeof selector === "object")
				jQuerySelect = $(selector);
			else
				throw TypeError("the first argument of textHighlight must either be a string selector, a Node, a NodeList or a jQuery Object");
			return this.on("input", function() {
				Search.highlight(this.value, jQuerySelect, caseSensitive);
			});
		},
		highlight: function(search, jQuerySelect, caseSensitive)
		{
			// on every keystroke, clean the selected elements by removing the highlighting markup
			// we will need to call the dom normalize() function to normalize text nodes 
			// (called in findAllTextNodes())
			$('.text-highlight-on, .text-highlight-off').each(function() {
				$(this).replaceWith($(this).html());
			});			
			if (!search) return;
			this.findAllTextNodes(search, jQuerySelect).forEach(function(node) {
				var text = node.nodeValue;
				// get all the matches positions for this text node and this search value
				var indexes = Search.findMatchPositions(text, search, caseSensitive);
				var previous = 0, before, highlight, after;
				if (!indexes.length) return;
				// we run through the matches positions and add highlighting markup
				// we do this by inserting new elements just before the targeted element
				// Content is thus duplicate and we use node.remove() at the end of the loop
				for (var i = 0; i < indexes.length; i++) {
					before = text.substr(previous, indexes[i] - previous);
					highlight = text.substr(indexes[i], search.length);
					$('<span class="text-highlight-off">' + before + "</span>").insertBefore(node);
					$('<mark class="text-highlight-on">' + highlight + "</mark>").insertBefore(node);
					previous = indexes[i] + search.length;
				}
				after = text.substr(previous);
				$('<span class="text-highlight-off">' + after + "</span>").insertBefore(node);
				// content is now duplicated so we need to remove the old node
				node.remove();
			});
		},
		// given a text and a search value we give the position of every match found
		// returns an array of positions
		findMatchPositions: function(text, search, caseSensitive)
		{
			var indexes = [];
			var flags = caseSensitive ? "g" : "gi";
			var reg = new RegExp(search, flags);
			while (match = reg.exec(text))
				indexes.push(match.index);
			return indexes;
		},
		// given a jQuery object we find all text nodes under each node in the object
		// returns all the nodes in an array
		findAllTextNodes: function(search, jQuerySelect)
		{
			var textNodes = [];
			jQuerySelect.each(function() {
				// here we call normalize on the element to reassemble text nodes
				// necessary to make new searches
				this.normalize();
				textNodes = textNodes.concat(Search.findChildrenTextNodes(this));
			});
			return textNodes;
		},

		// this function recursively finds all text nodes under a selected node
		// returns an array of text nodes
		findChildrenTextNodes: function(node)
		{
			var all = [];
			for (node = node.firstChild; node; node = node.nextSibling) {
				if (node.nodeType == 3) all.push(node);
				else all = all.concat(this.findChildrenTextNodes(node));
			}
			return all;
		}
	};
	$.fn.textHighlight = Search.init;
})();
