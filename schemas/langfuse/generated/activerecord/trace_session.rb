class TraceSession < ApplicationRecord
  self.primary_key = %w[id project_id]

  attribute :id, default: -> { Cuid.generate }
  attribute :bookmarked, default: false
  attribute :public, default: false
  attribute :environment, default: "default"

  belongs_to :project, inverse_of: :sessions
end
