class View < ApplicationRecord
  self.table_name = "view"
  self.inheritance_column = nil

  alias_attribute :updated_at, :last_modified_time

  validates :name, presence: true
  validates :type, presence: true
  validates :order, presence: true
  validates :version, presence: true
  validates :column_meta, presence: true
  validates :share_id, uniqueness: { allow_nil: true }
  validates :created_by, presence: true

  belongs_to :table, class_name: "TableMeta", inverse_of: :views
end
