class Account < ApplicationRecord
  self.table_name = "account"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :accountId, presence: true
  validates :providerId, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :accounts
end
