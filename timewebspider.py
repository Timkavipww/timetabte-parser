import scrapy


class TimewebspiderSpider(scrapy.Spider):
    name = "timewebspider"
    allowed_domains = ["timeweb.cloud"]
    start_urls = ["https://timeweb.cloud"]

    def parse(self, response):
        pass
