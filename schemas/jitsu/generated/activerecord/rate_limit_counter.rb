class RateLimitCounter < ApplicationRecord
  self.table_name = "RateLimitCounter"
  self.primary_key = %w[bucketKey windowStart]

  attribute :count, default: 0

  validates :expiresAt, presence: true
end
