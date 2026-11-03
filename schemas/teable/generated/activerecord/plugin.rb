class Plugin < ApplicationRecord
  self.table_name = "plugin"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :name, presence: true
  validates :logo, presence: true
  validates :status, presence: true
  validates :positions, presence: true
  validates :secret, presence: true, uniqueness: true
  validates :masked_secret, presence: true
  validates :created_by, presence: true

  has_many :plugin_install, dependent: :destroy
end
