class SessionRun < ApplicationRecord
  self.table_name = "SessionRun"

  attribute :id, default: -> { Cuid.generate }

  validates :runId, presence: true, uniqueness: true
  validates :reason, presence: true

  belongs_to :session, foreign_key: "sessionId", inverse_of: :runs
end
