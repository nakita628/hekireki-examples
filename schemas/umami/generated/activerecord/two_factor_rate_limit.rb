class TwoFactorRateLimit < ApplicationRecord
  self.table_name = "two_factor_rate_limit"

  attribute :id, default: -> { Cuid.generate }
  attribute :attempts, default: 0

  validates :user_id, uniqueness: true

  belongs_to :user
end
