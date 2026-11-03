class RegisteredDomain < ApplicationRecord
  self.table_name = "RegisteredDomain"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :renewalFee, default: 1200
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :slug, presence: true
  validates :domainId, uniqueness: { allow_nil: true }
  validates :expiresAt, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :registered_domains
  belongs_to :domain, foreign_key: "domainId", optional: true, inverse_of: :registered_domain
end
