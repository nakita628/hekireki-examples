class UserBaseOrder < ApplicationRecord
  self.table_name = "user_base_order"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :user_id, uniqueness: { scope: :base_id }
  validates :order, presence: true

  belongs_to :user, inverse_of: :base_orders
  belongs_to :base, inverse_of: :user_orders
end
