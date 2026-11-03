class OrganisationAuthenticationPortal < ApplicationRecord
  self.table_name = "OrganisationAuthenticationPortal"

  attribute :enabled, default: false
  attribute :clientId, default: ""
  attribute :clientSecret, default: ""
  attribute :wellKnownUrl, default: ""
  attribute :autoProvisionUsers, default: true
  attribute :allowedDomains, default: -> { [] }
  attribute :allowPersonalOrganisations, default: false

  enum :defaultOrganisationRole, { admin: "ADMIN", manager: "MANAGER", member: "MEMBER" }, default: :member,
                                                                                           validate: true

  has_one :organisation, foreign_key: "organisationAuthenticationPortalId",
                         inverse_of: :organisation_authentication_portal, dependent: :restrict_with_error
end
