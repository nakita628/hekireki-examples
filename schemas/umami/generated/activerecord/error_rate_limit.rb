class ErrorRateLimit < ApplicationRecord
  self.table_name = "error_rate_limit"
  self.primary_key = "website_id"

  validates :bucket, presence: true
  validates :count, presence: true
end
