class VirtualKey < ApplicationRecord
  self.table_name = "VirtualKey"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :metadata, default: -> { JSON.parse("{}") }
  attribute :config, default: -> { JSON.parse("{}") }
  attribute :revision, default: 0
  attribute :connectServices, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { active: "ACTIVE", disabled: "DISABLED", revoked: "REVOKED" }, default: :active, validate: true
  enum :purpose, { user: "USER", langy: "LANGY", connect: "CONNECT" }, default: :user, validate: true
  enum :routingMode, { none: "NONE", fallback_all: "FALLBACK_ALL", policy: "POLICY" }, default: :none, prefix: true,
                                                                                       validate: true

  validates :organizationId, presence: true,
                             uniqueness: { scope: :externalId, conditions: -> { where.not(externalId: nil) } }
  validates :name, presence: true
  validates :hashedSecret, presence: true, uniqueness: true
  validates :hashedSecret, uniqueness: true
  validates :displayPrefix, presence: true
  validates :createdById, presence: true
  validates :licenseTokenHash, uniqueness: { allow_nil: true }

  belongs_to :principal_user, class_name: "User", foreign_key: "principalUserId", optional: true,
                              inverse_of: :principal_virtual_keys
  belongs_to :routing_policy, foreign_key: "routingPolicyId", optional: true, inverse_of: :virtual_keys
  has_many :scopes, class_name: "VirtualKeyScope", foreign_key: "virtualKeyId", inverse_of: :virtual_key,
                    dependent: :destroy
  has_many :ledger_entries, class_name: "GatewayBudgetLedger", foreign_key: "virtualKeyId", inverse_of: :virtual_key,
                            dependent: :destroy
end
