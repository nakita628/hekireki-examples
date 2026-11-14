class OrganizationAccessNotice < ApplicationRecord
  self.table_name = "OrganizationAccessNotice"

  attribute :idpRefused, default: false
  alias_attribute :created_at, :createdAt

  validates :organizationId, presence: true, uniqueness: { scope: [ :kind, :email, :dayKey ] }
  validates :kind, presence: true
  validates :email, presence: true
  validates :dayKey, presence: true
end
