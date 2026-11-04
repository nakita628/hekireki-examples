class Base < ApplicationRecord
  self.table_name = "base"

  attribute :id, default: -> { Cuid.generate }
  attribute :v2_enabled, default: false
  alias_attribute :updated_at, :last_modified_time

  enum :provision_state, { pending: "pending", ready: "ready", error: "error", deleting: "deleting" }, default: :ready,
                                                                                                       validate: true

  validates :order, presence: true
  validates :created_by, presence: true

  belongs_to :space, inverse_of: :base_group
  has_many :tables, class_name: "TableMeta", inverse_of: :base, dependent: :restrict_with_error
  has_many :data_db_move_jobs, class_name: "BaseDataDbMoveJob", inverse_of: :base, dependent: :destroy
  has_many :user_orders, class_name: "UserBaseOrder", inverse_of: :base, dependent: :destroy
end
