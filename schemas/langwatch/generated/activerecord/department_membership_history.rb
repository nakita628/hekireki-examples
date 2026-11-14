class DepartmentMembershipHistory < ApplicationRecord
  self.table_name = "DepartmentMembershipHistory"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  validates :organizationId, presence: true
  validates :userId, presence: true
  validates :departmentId, presence: true
  validates :validFrom, presence: true
end
