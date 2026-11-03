class Access < ApplicationRecord
  self.table_name = "Access"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :scopes, default: -> { [] }
  attribute :settings, default: -> { JSON.parse("{}") }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { mcp: "MCP", private: "PRIVATE", public: "PUBLIC" }, default: :private, prefix: true, validate: true

  validates :expiresAt, presence: true

  belongs_to :grantee_user, class_name: "User", foreign_key: "granteeUserId", optional: true, inverse_of: :accesses_get
  belongs_to :user, foreign_key: "userId", inverse_of: :accesses_give
end
