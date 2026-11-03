class PluginContextMenu < ApplicationRecord
  self.table_name = "plugin_context_menu"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :plugin_install_id, presence: true, uniqueness: true
  validates :order, presence: true
  validates :created_by, presence: true

  belongs_to :table, class_name: "TableMeta", inverse_of: :plugin_context_menu
end
