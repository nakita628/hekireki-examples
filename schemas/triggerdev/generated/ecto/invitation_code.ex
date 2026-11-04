defmodule Trigger.InvitationCode do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          code: String.t(),
          inserted_at: DateTime.t(),
          users: [Trigger.User.t()]
        }

  schema "InvitationCode" do
    field(:id, :string, primary_key: true)
    field(:code, :string)
    has_many(:users, Trigger.User, foreign_key: :invitation_code_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end