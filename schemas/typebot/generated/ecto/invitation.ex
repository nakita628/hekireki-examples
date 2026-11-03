defmodule Typebot.Invitation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          typebot_id: String.t(),
          email: String.t(),
          type: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          typebot: Typebot.Typebot.t() | nil
        }

  schema "Invitation" do
    field(:email, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:READ, :WRITE, :FULL_ACCESS])
    field(:typebot_id, :string, primary_key: true, source: :typebotId)
    belongs_to(:typebot, Typebot.Typebot, foreign_key: :typebot_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end