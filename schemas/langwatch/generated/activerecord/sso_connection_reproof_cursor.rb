class SsoConnectionReproofCursor < ApplicationRecord
  self.table_name = "SsoConnectionReproofCursor"
  self.primary_key = "connectionId"

  validates :lastReproofAt, presence: true

  belongs_to :connection, class_name: "SsoConnection", foreign_key: "connectionId", inverse_of: :reproof_cursor
end
