class Memories < ApplicationRecord
  attribute :scope, default: "workspace"
  attribute :last_used_at, PrismaDateTime.new
  attribute :created_at, PrismaDateTime.new, default: -> { Time.current }
  attribute :updated_at, PrismaDateTime.new, default: -> { Time.current }

  validates :content, presence: true

  belongs_to :user, class_name: "Users", optional: true, inverse_of: :memories
  belongs_to :workspace, class_name: "Workspaces", optional: true, inverse_of: :memories
end
