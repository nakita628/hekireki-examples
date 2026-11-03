class User < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :name, presence: true
  validates :phone, uniqueness: { allow_nil: true }
  validates :email, presence: true, uniqueness: true

  has_many :accounts, dependent: :destroy
  has_many :base_orders, class_name: "UserBaseOrder", inverse_of: :user, dependent: :destroy
end
