class AttributeToUser < ApplicationRecord
  self.table_name = "AttributeToUser"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :memberId, uniqueness: { scope: :attributeOptionId }

  belongs_to :member, class_name: "Membership", foreign_key: "memberId", inverse_of: :attribute_to_user
  belongs_to :attribute_option, foreign_key: "attributeOptionId", inverse_of: :assigned_users
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", optional: true,
                          inverse_of: :created_attribute_to_users
  belongs_to :created_by_dsync, class_name: "DSyncData", foreign_key: "createdByDSyncId", primary_key: "directoryId",
                                optional: true, inverse_of: :created_attribute_to_users
  belongs_to :updated_by, class_name: "User", foreign_key: "updatedById", optional: true,
                          inverse_of: :updated_attribute_to_users
  belongs_to :updated_by_dsync, class_name: "DSyncData", foreign_key: "updatedByDSyncId", primary_key: "directoryId",
                                optional: true, inverse_of: :updated_attribute_to_users
end
