class ManagedOrganization < ApplicationRecord
  self.table_name = "ManagedOrganization"

  alias_attribute :created_at, :createdAt

  validates :managedOrganizationId, uniqueness: true
  validates :managerOrganizationId, uniqueness: { scope: :managedOrganizationId }

  belongs_to :managed_organization, class_name: "Team", foreign_key: "managedOrganizationId",
                                    inverse_of: :managed_organization
  belongs_to :manager_organization, class_name: "Team", foreign_key: "managerOrganizationId",
                                    inverse_of: :managed_organizations
end
