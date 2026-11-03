class DSyncData < ApplicationRecord
  self.table_name = "DSyncData"

  validates :directoryId, presence: true, uniqueness: true
  validates :tenant, presence: true
  validates :organizationId, uniqueness: { allow_nil: true }

  belongs_to :org, class_name: "OrganizationSettings", foreign_key: "organizationId", primary_key: "organizationId",
                   optional: true, inverse_of: :d_sync_data
  has_many :team_group_mapping, class_name: "DSyncTeamGroupMapping", foreign_key: "directoryId",
                                primary_key: "directoryId", inverse_of: :directory, dependent: :destroy
  has_many :created_attribute_to_users, class_name: "AttributeToUser", foreign_key: "createdByDSyncId",
                                        primary_key: "directoryId", inverse_of: :created_by_dsync, dependent: :nullify
  has_many :updated_attribute_to_users, class_name: "AttributeToUser", foreign_key: "updatedByDSyncId",
                                        primary_key: "directoryId", inverse_of: :updated_by_dsync, dependent: :nullify
end
