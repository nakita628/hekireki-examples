class Field < ApplicationRecord
  self.table_name = "field"
  self.inheritance_column = nil

  alias_attribute :updated_at, :last_modified_time

  enum :provision_state, { pending: "pending", ready: "ready", error: "error", deleting: "deleting" }, default: :ready,
                                                                                                       validate: true

  validates :name, presence: true
  validates :type, presence: true
  validates :cell_value_type, presence: true
  validates :db_field_type, presence: true
  validates :db_field_name, presence: true
  validates :order, presence: true
  validates :version, presence: true
  validates :created_by, presence: true

  belongs_to :table, class_name: "TableMeta", inverse_of: :fields
end
