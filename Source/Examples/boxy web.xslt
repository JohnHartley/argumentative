<?xml version="1.0" encoding="UTF-8"?>
<!-- boxy web.xslt produces a web page which shows the structure through a series of nested boxes -->
<!-- Version 0.1 November 2006 -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output
		method="html"
		doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"
		doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN"
	/>
	
	<xsl:template match="/">
		<html>
			<head>
				<title><xsl:value-of select="//premise/title"/></title>
				<meta name="description" content="{//premise/title}" />
				<meta name="keywords" content="Argument map;{//premise/title}" />
			
			</head>
			<body>
				<h1><xsl:value-of select="//premise/title"/></h1>
				<xsl:apply-templates select="*"/>
				<p>
				<xsl:text>Author: </xsl:text>
				<xsl:value-of select="//author" />
				</p>
			</body>
		</html>
	</xsl:template>
	
	<xsl:template match="premise">
		<div class="premise">
			<xsl:apply-templates select="reason|objection|helper"/>
		</div>
	</xsl:template>
	
	<xsl:template match="reason|objection|helper">
		<div style="margin:6mm; border:thin solid grey; padding-left:6mm">
			<strong>
				<xsl:value-of select="translate(substring(name(),1,1),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
				<xsl:value-of select="substring(name(),2,string-length(name()))"/>
				<xsl:text>: </xsl:text>
			</strong>
			<xsl:value-of select="title"/>
			
			<xsl:apply-templates select="reason|objection|helper"/>
		</div>
	</xsl:template>
	
	<xsl:template match="text()"/>
</xsl:stylesheet>
