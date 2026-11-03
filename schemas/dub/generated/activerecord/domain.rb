class Domain < ApplicationRecord
  self.table_name = "Domain"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :verified, default: false
  attribute :primary, default: false
  attribute :archived, default: false
  attribute :deepviewData, default: -> { JSON.parse("{}") }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :slug, presence: true, uniqueness: true

  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :domains
  has_one :registered_domain, foreign_key: "domainId", inverse_of: :domain, dependent: :nullify
  has_many :links, foreign_key: "domain", primary_key: "slug", inverse_of: :short_domain,
                   dependent: :restrict_with_error
end
