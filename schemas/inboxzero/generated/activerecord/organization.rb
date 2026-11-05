class Organization < ApplicationRecord
  self.table_name = "Organization"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: true

  has_one :sso_provider, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :members, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :sessions, foreign_key: "activeOrganizationId", inverse_of: :active_organization, dependent: :nullify
  has_many :invitations, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :rules, class_name: "OrganizationRule", foreign_key: "organizationId", inverse_of: :organization,
                   dependent: :destroy
  has_many :conversations, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
end
