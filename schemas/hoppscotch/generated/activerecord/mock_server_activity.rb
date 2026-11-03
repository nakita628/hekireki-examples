class MockServerActivity < ApplicationRecord
  self.table_name = "MockServerActivity"

  attribute :id, default: -> { Cuid.generate }

  enum :action, { created: "CREATED", deleted: "DELETED", activated: "ACTIVATED", deactivated: "DEACTIVATED" },
       validate: true

  belongs_to :mock_server, foreign_key: "mockServerID", inverse_of: :activity_history
end
