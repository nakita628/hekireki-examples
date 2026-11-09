class EmailLog < ApplicationRecord
  self.table_name = "EmailLog"

  attribute :id, default: -> { Cuid.generate }

  validates :email, exclusion: { in: [ nil ] }
  validates :status, presence: true
end
