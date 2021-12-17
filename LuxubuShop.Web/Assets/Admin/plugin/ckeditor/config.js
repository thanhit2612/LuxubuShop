/**
 * @license Copyright (c) 2003-2021, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
	config.toolbarGroups = [
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'insert', groups: ['insert'] },
		{ name: 'paragraph', groups: ['align', 'list', 'blocks', 'bidi', 'indent', 'paragraph'] },
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'forms', groups: ['forms'] },
		{ name: 'links', groups: ['links'] },
		{ name: 'tools', groups: ['tools'] },
		'/',
		'/',
		{ name: 'others', groups: ['others'] },
		{ name: 'about', groups: ['about'] }
	];

	
	config.removeButtons = 'Save,NewPage,Print,Templates,Preview,Styles,Subscript,Superscript,Smiley,SpecialChar,Iframe,PageBreak,CreateDiv,Language,BidiRtl,BidiLtr,Find,Replace,SelectAll,Cut,Copy,Paste,PasteText,PasteFromWord,Form,Checkbox,Radio,TextField,Textarea,Select,ImageButton,HiddenField,Button,Anchor,ShowBlocks,About';

	config.syntaxhighlight_lang = 'csharp';
	config.syntaxhighlight_hideControls = true;
	config.language = 'vi';
	config.filebrowserBrowseUrl = '/Assets/Admin/plugin/ckfinder/ckfinder.html';
	config.filebrowserImageBrowseUrl = '/Assets/Admin/plugin/ckfinder.html?Type=Images';
	config.filebrowserFlashBrowseUrl = '/Assets/Admin/plugin/ckfinder.html?Type=Flash';
	config.filebrowserUploadUrl = '/Assets/Admin/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
	config.filebrowserImageUploadUrl = '/UploadFiles';
	config.filebrowserFlashUploadUrl = '/Assets/Admin/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

	CKFinder.setupCKEditor(null, '/Assets/Admin/plugin/ckfinder/');
};
