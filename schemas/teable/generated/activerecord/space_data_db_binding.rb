class SpaceDataDbBinding < ApplicationRecord
  self.table_name = "space_data_db_binding"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  enum :mode, { default: "default", byodb: "byodb" }, default: :default, validate: true
  enum :state, { ready: "ready", validating: "validating", initializing: "initializing", migrating: "migrating", error: "error", disabled: "disabled" },
       default: :ready, validate: true

  validates :space_id, uniqueness: true
  validates :created_by, presence: true

  belongs_to :space, inverse_of: :data_db_binding
  belongs_to :data_db_connection, optional: true, inverse_of: :space_bindings
end
