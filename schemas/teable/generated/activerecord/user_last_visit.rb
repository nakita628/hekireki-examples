class UserLastVisit < ApplicationRecord
  self.table_name = "user_last_visit"

  attribute :id, default: -> { Cuid.generate }

  validates :user_id, presence: true, uniqueness: { scope: %i[resource_type resource_id] }
  validates :resource_type, presence: true
  validates :resource_id, presence: true
  validates :parent_resource_id, presence: true
end
