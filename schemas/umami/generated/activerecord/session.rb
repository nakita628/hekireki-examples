class Session < ApplicationRecord
  self.table_name = "session"
  self.primary_key = "session_id"

  validates :website_id, presence: true
  validates :browser, length: { maximum: 20 }
  validates :os, length: { maximum: 20 }
  validates :device, length: { maximum: 20 }
  validates :screen, length: { maximum: 11 }
  validates :language, length: { maximum: 35 }
  validates :country, length: { maximum: 2 }
  validates :region, length: { maximum: 20 }
  validates :city, length: { maximum: 50 }
  validates :distinct_id, length: { maximum: 50 }

  has_many :website_events, dependent: :restrict_with_error
  has_many :session_data, class_name: "SessionData", inverse_of: :session, dependent: :restrict_with_error
  has_many :revenue, dependent: :restrict_with_error
end
