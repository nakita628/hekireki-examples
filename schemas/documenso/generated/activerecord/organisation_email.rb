class OrganisationEmail < ApplicationRecord
  self.table_name = "OrganisationEmail"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :email, presence: true, uniqueness: true
  validates :emailName, presence: true

  belongs_to :email_domain, foreign_key: "emailDomainId", inverse_of: :emails
  belongs_to :organisation, foreign_key: "organisationId", inverse_of: :organisation_emails
  has_many :organisation_global_settings, foreign_key: "emailId", inverse_of: :email, dependent: :nullify
  has_many :team_global_settings, foreign_key: "emailId", inverse_of: :email, dependent: :nullify
end
