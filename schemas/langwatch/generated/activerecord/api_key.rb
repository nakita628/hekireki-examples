class ApiKey < ApplicationRecord
  self.table_name = "ApiKey"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :permissionMode, default: "all"
  attribute :isSystemManaged, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :lookupId, presence: true, uniqueness: true
  validates :hashedSecret, presence: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :api_keys
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :api_keys
  has_many :role_bindings, foreign_key: "apiKeyId", inverse_of: :api_key, dependent: :restrict_with_error
end
