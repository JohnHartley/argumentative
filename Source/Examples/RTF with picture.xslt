<?xml version="1.0" encoding="UTF-8" ?>
<!-- Exports an Argument (*.axl) file to a RTF (Rich Text Format) file that may be opened by MS Word -->
<!-- This example includes a picture -->
<!-- Note: if the text contains \ or { } symbols they need to be preceeded by a \ -->
<!-- Version 0.2 20 Jan 2008 -->
<xsl:stylesheet version = '1.0' 
                xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
                xmlns:argImage="urn:image">
	<xsl:output method="text"/>
	
	<xsl:template match="/">
	<xsl:text>{\rtf1\ansi\ansicpg1252\deff0\deflang3081{\fonttbl{\f0\fswiss\fcharset0 Arial;}}</xsl:text>
	<xsl:text>
</xsl:text>
	<xsl:text>\viewkind4\uc1\pard\f0\fs20 </xsl:text>
	
	<xsl:text>\par </xsl:text>
	<!-- <xsl:value-of select="//pictureData"/> -->
	<xsl:value-of select="argImage:ImageToRTF('')"/>
	<xsl:text>\par </xsl:text>
	<!-- Render Premise, reasons, objections and helpers -->
	<xsl:apply-templates select="argument"/>
	<xsl:text>\par </xsl:text>
	<!-- Define a footer with a font size of 8pt (16 half points) i.e. \fs16 -->
	<xsl:text>\footer \fs16</xsl:text>
	<xsl:text>\par Long Date: </xsl:text>
	<xsl:value-of select="//longDate"/>
	<xsl:text>  Long Time: </xsl:text>
	<xsl:value-of select="//longTime"/>
	<xsl:text>\par Short Date: </xsl:text>
	<xsl:value-of select="//shortDate"/>
	<xsl:text>  Short Time: </xsl:text>
	<xsl:value-of select="//shortTime"/>
	<xsl:text>}</xsl:text>
	</xsl:template>
	
	<xsl:template match="premise">
		<xsl:text> \b Premise: \b0 </xsl:text>
		<xsl:value-of select="title"/>
		<xsl:text>\par \par

</xsl:text>
		<xsl:apply-templates select="reason|objection|helper"/>
	</xsl:template>
	
	<xsl:template match="reason">
		<xsl:number level="multiple" count="reason|objection|helper" />
		<xsl:text> \b Reason: \b0 </xsl:text>
		<xsl:value-of select="title"/>
		<xsl:text>\par</xsl:text>
		<xsl:value-of select="argImage:ImageToRTF(ref)"/>
		<xsl:text>\par \par

</xsl:text>
		<xsl:apply-templates select="reason|objection|helper"/>
	</xsl:template>
	
	<xsl:template match="objection">
		<xsl:number level="multiple" count="reason|objection|helper" />
		<xsl:text> \b Objection: \b0 </xsl:text>
		<xsl:value-of select="title"/>
		<xsl:text>\par</xsl:text>
		<xsl:value-of select="argImage:ImageToRTF(ref)"/>
		<xsl:text>\par \par

</xsl:text>
		<xsl:apply-templates select="reason|objection|helper"/>
	</xsl:template>
	
	<xsl:template match="helper">
		<xsl:number level="multiple" count="reason|objection|helper" />
		<xsl:text> \b Helper: \b0</xsl:text>
		<xsl:value-of select="title"/>
		<xsl:text>\par \par

</xsl:text>
		<xsl:apply-templates select="reason|objection|helper"/>
	</xsl:template>
	
	<xsl:template match="text()"/>
</xsl:stylesheet>
