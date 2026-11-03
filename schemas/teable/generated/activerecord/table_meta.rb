class TableMeta < ApplicationRecord
  alias_attribute :updated_at, :last_modified_time

  enum :provision_state, { pending: "pending", ready: "ready", error: "error", deleting: "deleting" }, default: :ready,
                                                                                                       validate: true

  validates :name, presence: true
  validates :db_table_name, presence: true
  validates :version, presence: true
  validates :order, presence: true
  validates :created_by, presence: true

  belongs_to :base, inverse_of: :tables
  has_many :fields, foreign_key: "table_id", inverse_of: :table, dependent: :restrict_with_error
  has_many :views, foreign_key: "table_id", inverse_of: :table, dependent: :restrict_with_error
  has_many :plugin_panel, class_name: "PluginPanel", foreign_key: "table_id", inverse_of: :table, dependent: :destroy
  has_many :plugin_context_menu, class_name: "PluginContextMenu", foreign_key: "table_id", inverse_of: :table,
                                 dependent: :destroy
end
