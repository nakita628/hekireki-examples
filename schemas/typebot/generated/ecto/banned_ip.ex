defmodule Typebot.BannedIp do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          ip: String.t(),
          inserted_at: DateTime.t(),
          responsible_typebot: Typebot.Typebot.t() | nil,
          user: Typebot.User.t() | nil
        }

  schema "BannedIp" do
    field(:id, :string, primary_key: true)
    field(:ip, :string)
    field(:responsible_typebot_id, :string, source: :responsibleTypebotId)
    field(:user_id, :string, source: :userId)
    belongs_to(:responsible_typebot, Typebot.Typebot, foreign_key: :responsible_typebot_id, define_field: false, type: :string)
    belongs_to(:user, Typebot.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end