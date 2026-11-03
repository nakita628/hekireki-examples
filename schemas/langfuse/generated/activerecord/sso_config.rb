class SsoConfig < ApplicationRecord
  self.primary_key = "domain"
  self.implicit_order_column = "created_at"

  attribute :domain, default: -> { Cuid.generate }

  validates :auth_provider, presence: true
end
