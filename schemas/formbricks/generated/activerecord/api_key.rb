class ApiKey < ApplicationRecord
  self.table_name = "ApiKey"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :organizationAccess, default: -> { JSON.parse("{}") }
  alias_attribute :created_at, :createdAt

  validates :label, presence: true
  validates :hashedKey, presence: true
  validates :lookupHash, uniqueness: { allow_nil: true }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :api_keys
  has_many :api_key_workspaces, foreign_key: "apiKeyId", inverse_of: :api_key, dependent: :destroy
end
