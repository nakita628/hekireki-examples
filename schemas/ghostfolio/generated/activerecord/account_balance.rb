class AccountBalance < ApplicationRecord
  self.table_name = "AccountBalance"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :date, uniqueness: { scope: [ :accountId, :userId ] }
  validates :value, presence: true

  belongs_to :account, foreign_key: [ :accountId, :userId ], inverse_of: :balances
end
