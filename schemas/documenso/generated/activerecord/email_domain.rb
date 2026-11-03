class EmailDomain < ApplicationRecord
  self.table_name = "EmailDomain"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", active: "ACTIVE" }, default: :pending, validate: true

  validates :selector, presence: true, uniqueness: true
  validates :domain, presence: true, uniqueness: true
  validates :publicKey, presence: true
  validates :privateKey, presence: true

  belongs_to :organisation, foreign_key: "organisationId", inverse_of: :email_domains
  has_many :emails, class_name: "OrganisationEmail", foreign_key: "emailDomainId", inverse_of: :email_domain,
                    dependent: :destroy
end
