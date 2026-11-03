class PluginInstall < ApplicationRecord
  self.table_name = "plugin_install"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :base_id, presence: true
  validates :name, presence: true
  validates :position_id, presence: true
  validates :position, presence: true
  validates :created_by, presence: true

  belongs_to :plugin, inverse_of: :plugin_install
end
