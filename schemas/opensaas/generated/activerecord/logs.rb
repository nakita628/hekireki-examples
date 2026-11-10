class Logs < ApplicationRecord
  self.table_name = "Logs"

  alias_attribute :created_at, :createdAt

  validates :message, presence: true
  validates :level, presence: true
end
