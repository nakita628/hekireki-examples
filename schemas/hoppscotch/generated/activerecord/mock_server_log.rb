class MockServerLog < ApplicationRecord
  self.table_name = "MockServerLog"

  attribute :id, default: -> { Cuid.generate }

  validates :requestMethod, presence: true
  validates :requestPath, presence: true
  validates :requestHeaders, exclusion: { in: [ nil ] }
  validates :responseStatus, presence: true
  validates :responseHeaders, exclusion: { in: [ nil ] }
  validates :responseTime, presence: true

  belongs_to :mock_server, foreign_key: "mockServerID", inverse_of: :request_logs
end
