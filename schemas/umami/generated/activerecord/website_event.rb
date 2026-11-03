class WebsiteEvent < ApplicationRecord
  self.table_name = "website_event"
  self.primary_key = "event_id"

  attribute :event_type, default: 1

  validates :website_id, presence: true
  validates :visit_id, presence: true
  validates :url_path, presence: true, length: { maximum: 500 }
  validates :url_query, length: { maximum: 500 }
  validates :utm_source, length: { maximum: 255 }
  validates :utm_medium, length: { maximum: 255 }
  validates :utm_campaign, length: { maximum: 255 }
  validates :utm_content, length: { maximum: 255 }
  validates :utm_term, length: { maximum: 255 }
  validates :referrer_path, length: { maximum: 500 }
  validates :referrer_query, length: { maximum: 500 }
  validates :referrer_domain, length: { maximum: 500 }
  validates :page_title, length: { maximum: 500 }
  validates :gclid, length: { maximum: 255 }
  validates :fbclid, length: { maximum: 255 }
  validates :msclkid, length: { maximum: 255 }
  validates :ttclid, length: { maximum: 255 }
  validates :li_fat_id, length: { maximum: 255 }
  validates :twclid, length: { maximum: 255 }
  validates :event_name, length: { maximum: 50 }
  validates :tag, length: { maximum: 50 }
  validates :hostname, length: { maximum: 100 }

  belongs_to :session, inverse_of: :website_events
  has_many :event_data, dependent: :restrict_with_error
end
