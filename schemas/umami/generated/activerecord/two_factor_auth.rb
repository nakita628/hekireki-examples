class TwoFactorAuth < ApplicationRecord
  self.table_name = "two_factor_auth"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :is_enabled, default: false

  validates :user_id, uniqueness: true
  validates :secret, presence: true

  belongs_to :user
end
