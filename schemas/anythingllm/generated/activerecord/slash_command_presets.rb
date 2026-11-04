class SlashCommandPresets < ApplicationRecord
  attribute :uid, default: 0
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :command, presence: true
  validates :prompt, presence: true
  validates :description, presence: true
  validates :uid, uniqueness: { scope: :command }

  belongs_to :user, class_name: "Users", foreign_key: "userId", optional: true, inverse_of: :slash_command_presets
end
