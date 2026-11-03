class OAuthClientApproval < ApplicationRecord
  self.table_name = "OAuthClientApproval"

  attribute :id, default: -> { Cuid.generate }

  validates :userId, presence: true, uniqueness: { scope: :clientId }
  validates :clientId, presence: true
end
