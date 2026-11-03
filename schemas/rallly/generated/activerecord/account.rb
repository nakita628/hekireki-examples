class Account < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :provider, presence: true, uniqueness: { scope: :provider_account_id }
  validates :provider_account_id, presence: true

  belongs_to :user, inverse_of: :accounts
end
