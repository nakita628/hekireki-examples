class ErrorEvent < ApplicationRecord
  self.table_name = "error_event"
  self.primary_key = %w[website_id event_id]

  validates :issue_id, presence: true
  validates :session_id, presence: true
  validates :visit_id, presence: true
  validates :name, presence: true, length: { maximum: 200 }
  validates :message, presence: true, length: { maximum: 2000 }
  validates :stack, presence: true
  validates :frames, exclusion: { in: [ nil ] }
  validates :tags, exclusion: { in: [ nil ] }
  validates :handled, inclusion: { in: [ true, false ] }
  validates :url_path, presence: true, length: { maximum: 500 }
  validates :release, presence: true, length: { maximum: 100 }
  validates :environment, presence: true, length: { maximum: 50 }
  validates :browser, presence: true, length: { maximum: 50 }
  validates :os, presence: true, length: { maximum: 50 }
  validates :device, presence: true, length: { maximum: 50 }
end
