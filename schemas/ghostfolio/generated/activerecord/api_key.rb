class ApiKey < ApplicationRecord
  self.table_name = "ApiKey"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :hashedKey, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :api_keys
end
