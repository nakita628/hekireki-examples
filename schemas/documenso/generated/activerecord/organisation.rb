class Organisation < ApplicationRecord
  self.table_name = "Organisation"
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { personal: "PERSONAL", organisation: "ORGANISATION" }, validate: true

  validates :name, presence: true
  validates :url, presence: true, uniqueness: true
  validates :customerId, uniqueness: { allow_nil: true }
  validates :organisationClaimId, uniqueness: true
  validates :organisationGlobalSettingsId, uniqueness: true
  validates :organisationAuthenticationPortalId, uniqueness: true

  belongs_to :organisation_claim, foreign_key: "organisationClaimId", inverse_of: :organisation
  belongs_to :avatar_image, foreign_key: "avatarImageId", optional: true, inverse_of: :organisation
  belongs_to :owner, class_name: "User", foreign_key: "ownerUserId", inverse_of: :owned_organisations
  belongs_to :organisation_global_settings, foreign_key: "organisationGlobalSettingsId", inverse_of: :organisation
  belongs_to :organisation_authentication_portal, foreign_key: "organisationAuthenticationPortalId",
                                                  inverse_of: :organisation
  has_one :subscription, foreign_key: "organisationId", inverse_of: :organisation, dependent: :destroy
  has_many :members, class_name: "OrganisationMember", foreign_key: "organisationId", inverse_of: :organisation,
                     dependent: :destroy
  has_many :invites, class_name: "OrganisationMemberInvite", foreign_key: "organisationId", inverse_of: :organisation,
                     dependent: :destroy
  has_many :groups, class_name: "OrganisationGroup", foreign_key: "organisationId", inverse_of: :organisation,
                    dependent: :destroy
  has_many :teams, foreign_key: "organisationId", inverse_of: :organisation, dependent: :destroy
  has_many :email_domains, foreign_key: "organisationId", inverse_of: :organisation, dependent: :destroy
  has_many :organisation_emails, foreign_key: "organisationId", inverse_of: :organisation, dependent: :destroy
  has_many :monthly_stats, class_name: "OrganisationMonthlyStat", foreign_key: "organisationId",
                           inverse_of: :organisation, dependent: :destroy
  has_many :embed_signatures, foreign_key: "organisationId", inverse_of: :organisation, dependent: :destroy
end
