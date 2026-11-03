class PluginPanel < ApplicationRecord
  self.table_name = "plugin_panel"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :name, presence: true
  validates :created_by, presence: true

  belongs_to :table, class_name: "TableMeta", inverse_of: :plugin_panel
end
